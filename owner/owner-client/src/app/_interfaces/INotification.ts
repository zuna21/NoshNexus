export interface INotification {
    id: string;
    title: string;
    description: string;
    isSeen: boolean;
    createdAt: Date;
}

export interface INotificationsForMenu {
    notSeenNumber: number;
    notifications: INotification[];
}
