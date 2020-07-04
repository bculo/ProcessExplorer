import { IPaginationResponse } from '../models/interfaces.models';

export interface IPageChanger {
    back(): void;
    next(): void;
    hasMore(): boolean;
    canGoBack(): boolean;
    getTotalPages(): number;
    getCurrentPage(): number;
}

export abstract class PaginationComponent<T> implements IPageChanger {

    public totalRecords: number = 0;
    public records: T[] = [];
    public currentPage: number = 1;
    public totalPages: number = 1;
    public searchCriteria: string = "";
    public errorMessage: string | null = null;
    public isLoading: boolean = true;

    abstract getRecords(): void;

    protected handleResponse(response: IPaginationResponse<T>): void{
        this.records = response.records;
        this.totalRecords = response.totalRecords;
        this.totalPages = response.totalPages;

        this.isLoading = false;
        this.errorMessage = null;
    }

    protected handleError(): void{
        this.errorMessage = "An error occurred";
        this.isLoading = false;
    }

    public back(): void{
        if(this.currentPage === 1) return;
    
        this.currentPage--;
        this.getRecords();
    }
    
    public next(): void {
        if(this.currentPage === this.totalPages) return;
    
        this.currentPage++;
        this.getRecords();
    }

    public hasMore(): boolean{
        if(this.currentPage === this.totalPages) 
            return false;
        return true;
    }

    public canGoBack(): boolean {
        if(this.currentPage === 1)
            return false;
        return true;
    }
    
    public onSearch(searchCriteria: string): void{
        if(this.searchCriteria !== searchCriteria) { 
          this.searchCriteria = searchCriteria;
          this.currentPage = 1;
          this.getRecords();
        }
    }

    public getTotalPages(): number {
        return this.totalPages;
    }

    public getCurrentPage(): number {
        return this.currentPage;
    }
}