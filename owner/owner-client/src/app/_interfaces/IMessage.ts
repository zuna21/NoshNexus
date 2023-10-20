

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


