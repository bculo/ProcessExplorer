import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

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

  private getControlErrorCode(fieldName: string): string[] {
    const controlErrors = this.form.get(fieldName).errors;
    let errorKeys = [];
    Object.keys(controlErrors).forEach(key => {
      errorKeys.push(key);
    });
    return errorKeys;
  }

  public getFirstErrorMessage(fieldName: string): string {
    const errorKeys = this.getControlErrorCode(fieldName);
    const firstErrorCode = errorKeys[0];
    switch (firstErrorCode) {
      case 'required': return `${fieldName} is required!`;
      case 'pattern': return `${fieldName} has wrong pattern!`;
      case 'email': return `${fieldName} has wrong email format!`; 
      case 'minlength': return `${fieldName} has wrong length!`; 
      default: return 'Error occurred';
    }
  }

  public removeForm(){
    this.form = null;
  }

  public displayFieldCss(fieldName: string) {
    return {
      'is-danger': this.isFieldValid(fieldName),
    };
  }
}
