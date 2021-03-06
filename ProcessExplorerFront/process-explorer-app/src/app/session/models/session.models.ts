import { IColumnChartDto } from 'src/app/shared/models/interfaces.models';

export interface IPieChartRecords {
    name: string[],
    quantity: number[]
}

export interface IMostActiveDay {
    date: string,
    numberOfSessions: number
}

export interface IActivityRecords {
    date: string[],
    number: number[],
}

export interface ISessionStatsResponse {
    pieChartRecords: IPieChartRecords;
    mostActiveDay: IMostActiveDay;
    activityChartRecords: IActivityRecords
    totalNumberOfSessions: number;
    numberOfUsers: number
}

export interface ISessionItem {
    id: string;
    userName: string;
    os: string;
    started: Date;
    differentProcessesNumber: number;
    applicationNumber: number;
}

export interface ISingleSession {
    id: string;
    userName: string;
    os: string;
    started: Date;
}

export interface ISessionProcessItem {
    processName: string;
    googleSearchQuery: string;
}

export interface ISessionApplicationItem {
    name: string;
    opened: Date;
    closed: Date;
}

export interface ITopSessionApplications {
    chartRecords: IColumnChartDto;
}