export interface IChartModel {
    data: number[];
    labels: string[];
    title: boolean;
    type: string;
    colors: any[]
}

export interface IChartExtendedModel extends IChartModel {
    mainHeading: string;
}

export interface IPaginationResponse<T> {
    records: T[];
    totalRecords: number;
    totalPages: number;
}

export interface ILoadingMember<T>{
    data: T,
    isLoading: boolean;
    errorMessage: string | null;
} 

export interface IColumnChartDto {
    label: string[];
    value: number[];
}

export interface IPieChartDto {
    name: string[];
    quantity: number[];
}

export interface IOsStatisticResponse {
    pieChart: IPieChartDto;
}

export interface IChartMapper {
    map<T>(response: T, member: ILoadingMember<IChartExtendedModel>): void;
}
