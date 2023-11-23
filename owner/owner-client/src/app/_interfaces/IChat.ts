export interface IChatPreview {
  id: string;
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
  id: string;
  name: string;
  participants: IChatParticipant[];
  messages: IMessage[];
}

export interface IChatParticipant {
  id: string;
  username: string;
  profileImage: string;
}

export interface IMessage {
  id: string;
  content: string;
  sender: IChatSender;
  isMine: boolean;
  createdAt: Date;
}

export interface IChatSender {
  id: string;
  username: string;
  profileImage: string;
  isActive: boolean;
}

export interface ICreateChat {
  name: string;
  participantsId: number[];
}