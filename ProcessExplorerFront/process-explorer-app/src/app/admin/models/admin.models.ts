export interface ICommunicationTypeResponse {
    types: ICommunication[];
}

export interface ICommunication {
    type: number;
    name: string;
    isActive: boolean;
}