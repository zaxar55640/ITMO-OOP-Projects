using Isu.Services;

namespace Isu.Extra.Models;

public class FacultyName
{
    public FacultyName(string name)
    {
        if (name[0] != Convert.ToChar("M") && name[0] != Convert.ToChar("K"))
            throw new Dataexception();
        Faculty = name[0];
    }

    public char Faculty { get; }
}