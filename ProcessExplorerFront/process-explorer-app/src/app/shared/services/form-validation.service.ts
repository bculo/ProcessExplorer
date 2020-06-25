import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormValidationService {

  private form: FormGroup;

  constructor() { }

  public setForm(form: FormGroup){
    if(form === null)
      throw new Error('FormGroup is NULL');

    this.form = form;
  }

  public isFieldValid(fieldName: string): boolean {
    if(this.form === null)
      throw new Error('Call setForm() method first!!!');

    return !this.form.get(fieldName).valid && this.form.get(fieldName).touched;
  }

  private getControlErrorCodes(fieldName: string): string[] {
    const controlErrors = this.form.get(fieldName).errors;
    let errorCodes = [];
    Object.keys(controlErrors).forEach(key => {
      errorCodes.push(key);
    });
    return errorCodes;
  }

  private getError(errorCode: string, fieldName: string){
    return this.form.get(fieldName).getError(errorCode);
  }

  public getErrorMessage(fieldName: string): string {
    if(fieldName === null) return;
    const errorKeys = this.getControlErrorCodes(fieldName);
    const firstErrorCode = errorKeys[0];
    const capitalizedFieldName = this.capitalizeFirstCharacter(fieldName);
    const fieldValue = this.getControl(fieldName).value;
    switch (firstErrorCode) {
      case 'required': return `${capitalizedFieldName} must not be empty.`;
      case 'email': return `${capitalizedFieldName} has wrong email format!`; 
      case 'minlength': return `The length of ${capitalizedFieldName} must be at least ${this.getError(firstErrorCode, fieldName).requiredLength} characters! You entered  ${fieldValue.length}`;
      case 'server': return this.getError('server', fieldName);
      default: return 'This field is required';
    }
  }

  private capitalizeFirstCharacter(fieldName: string): string {
    if(fieldName.length === 1) 
      return fieldName.toUpperCase();
    return `${fieldName.charAt(0).toUpperCase()}${fieldName.slice(1)}`;
  }

  public removeForm(){
    this.form = null;
  }

  public displayFieldCss(fieldName: string) {
    return {
      'is-danger': this.isFieldValid(fieldName),
    };
  }

  private getControl(propertyName: string) {
    const lowerCase = propertyName.toLowerCase();
    return this.form.get(lowerCase);
  }

  public handleServerError(response: HttpErrorResponse){
    if(response.status === 0)
      return throwError("Server not available :(");

    if(response.status > 500 && response.status < 600)
      return throwError("Internal server error :(");

    if(response.status === 404)
      return throwError("Method not found :(");

    if(response.status === 400) {
      const error = response.error;

      //handle dictionary
      if (error.constructor == Object) {
        console.log(error)
        Object.keys(error).forEach(property => {
          let control = this.getControl(property);
          control.setErrors( {'server': error[property][0]} );
        });
        return throwError(null);
      }
      else if(Array.isArray(error)) {
        return throwError(error[0]);
      }
      else{
        return throwError(error);
      }
    }

    return throwError("Unknown error");
  }

}
