namespace ApplicationCore.DTOs;

public class ChatDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ChatParticipantDto> Participants { get; set; }
    public ICollection<MessageDto> Messages { get; set; }
}

public class ChatMenuDto
{
    public int NotSeenNumber { get; set; }
    public ICollection<ChatPreviewDto> Chats { get; set; }
}

public class MessageDto 
{
    public int Id { get; set; }
    public string Content { get; set; }
    public ChatSenderDto Sender { get; set; }
    public bool IsMine { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ChatParticipantDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string ProfileImage { get; set; }
}

public class CreateChatDto
{
    public string Name { get; set; }
    public ICollection<int> ParticipantsId { get; set; }
}

public class ChatPreviewDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSeen { get; set; }
    public ChatPreviewLastMessageDto LastMessage { get; set; }
}

public class CreateMessageDto
{
    public string Content { get; set; }
}

public class ChatPreviewLastMessageDto
{
    public string Content { get; set; }
    public ChatSenderDto Sender { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ChatSenderDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string ProfileImage { get; set; }
    public bool IsActive { get; set; }
}