﻿namespace API;

public interface IChatRepository
{
    Task<List<ChatParticipantDto>> GetUsersForChatParticipants(string likeUsername, int whoSearchId);
    Task<List<AppUser>> GetParticipantsById(ICollection<int> ids);
    Task<ICollection<ChatPreviewDto>> GetChats(int userId);
    Task<Chat> GetChatById(int chatId, int userId);
    Task<AppUserChat> GetAppUserChat(int chatId, int userId);
    Task<ICollection<AppUserChat>> GetAppUserChats(int chatId);
    Task<ICollection<AppUser>> GetChatParticipants(int chatId);
    Task<ICollection<MessageDto>> GetChatMessages(int chatId, int userId);
    Task<int> NotSeenNumber(int userId);
    void CreateMessage(Message message);
    void CreateChat(Chat chat);
    void CreateAppUserChats(List<AppUserChat> appUserChats);
    Task<bool> SaveAllAsync();
}