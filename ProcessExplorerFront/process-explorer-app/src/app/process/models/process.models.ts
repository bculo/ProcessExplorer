import { IPaginationResponse, IColumnChartDto } from 'src/app/shared/models/interfaces.models';

export interface IProcessItem {
    processName: string;
    googleSearchQuery: string;
    occuresInNumOfSessions: number;
}

export class IProcessPaginationResponseDto implements IPaginationResponse<IProcessItem> {
    records: IProcessItem[];
    totalRecords: number;
    totalPages: number;
    totalNumberOfSessions: number;
}

export interface IBestProcessesDay {
    day: Date;
    totalNumberOfDifferentProcesses: number;
}

export interface ITopProcessesPeriodResponseDto {
    chartRecords: IColumnChartDto;
    maxNumberOfSessions: number;
}
