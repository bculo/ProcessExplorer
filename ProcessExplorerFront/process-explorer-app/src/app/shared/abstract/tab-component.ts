export interface ITabItem {
    icon: string;
    title: string;
    route: string;
    exactRoute: boolean;
}

export interface ITabClient {
    tabItems: ITabItem[];
}