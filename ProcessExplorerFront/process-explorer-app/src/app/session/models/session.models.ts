export interface IPieChartItem {
    name: string,
    quantity: number
}

export interface ISessionStatsResponse {
    pieChartRecords: IPieChartItem[];
}