export interface IMessage {
  id: string;
  user: {
    id: string;
    username: string;
    profilePicture: string;
    isActive: boolean;
  };
  message: string;
  isSeen: boolean;
  createdAt: Date;
}

export interface IMessagesForMenu {
  notSeenNumber: number;
  messages: IMessage[];
}
