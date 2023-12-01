export interface IChatPreview {
  id: number;
  name: string;
  isSeen: boolean;
  lastMessage: {
    content: string;
    sender: IChatSender;
    createdAt: Date;
  };
}


export interface IChatMenu {
  notSeenNumber: number;
  chats: IChatPreview[];
}

export interface IChat {
  id: number;
  name: string;
  participants: IChatParticipant[];
  messages: IMessage[];
}

export interface IChatParticipant {
  id: number;
  username: string;
  profileImage: string;
}

export interface IMessage {
  id: number;
  content: string;
  sender: IChatSender;
  isMine: boolean;
  createdAt: Date;
}

export interface IChatSender {
  id: number;
  username: string;
  profileImage: string;
  isActive: boolean;
}

export interface ICreateChat {
  name: string;
  participantsId: number[];
}