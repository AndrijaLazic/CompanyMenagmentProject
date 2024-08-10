export class UserCommunication {
  id!: number;
  user1!: number;
  user2!: number;
  user1Unread!: number;
  user2Unread!: number;
  communicationMessages: Array<MessageDTR> = [];
}

export type MessageDTR = {
  id: number;
  communicationId: number;
  senderId: number;
  message: string;
};
