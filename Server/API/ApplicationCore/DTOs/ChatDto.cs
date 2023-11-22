namespace API;

public class ChatDto
{

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