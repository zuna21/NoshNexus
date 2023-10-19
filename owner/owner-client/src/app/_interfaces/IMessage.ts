export interface IMessage {
  id: string;
  user: {
    id: string;
    username: string;
    profilePicture: string;
    isActive: boolean;
  };
  message: string;
  isMine: boolean;
  createdAt: Date;
}

export interface IChat {
  id: string;
  name: string;
  participants: {
    id: string;
    username: string;
  }[];
  messages: IMessage[];
}

export interface IChatMenu {
  notSeenNumber: number;
  chats: IChatMenuSingle[];
}

export interface IChatMenuSingle {
  id: string;
  title: string;
  isSeen: boolean;
  lastMessage: {
    content: string;
    sender: string;
    createdAt: Date;
  };
}
