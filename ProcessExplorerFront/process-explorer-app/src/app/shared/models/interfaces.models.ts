export class IChartModel {
    data: number[];
    labels: string[];
    title: boolean;
    type: string;
    colors: any[];
}

export class IPaginationResponse<T> {
    records: T[];
    totalRecords: number;
    totalPages: number;
}