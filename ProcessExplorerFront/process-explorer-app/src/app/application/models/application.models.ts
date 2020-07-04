import { IColumnChartDto } from 'src/app/shared/models/interfaces.models';

export interface IBestApplicationDay {
    day: Date;
    number: number;
}

export interface ITopApplications {
    chartRecords: IColumnChartDto
}

export interface ISessionAppsChart {
    date: string[];
    number: number[];
}

export interface IOpenedAppsPerSessionResponse {
    chart: ISessionAppsChart;
}

export interface IApplicationItem {
    applicationName: string;
    googleSearchQuery: string;
    occuresNumOfTime: string;
}


