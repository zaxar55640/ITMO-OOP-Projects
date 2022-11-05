using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class GroupWithFaculty : Group
{
    private List<Pair> _pairs;
    private string _faculty;
    public GroupWithFaculty(string name)
        : base(name)
    {
        _faculty = Convert.ToString(name[0]);
        _pairs = new List<Pair>();
        ExStudents = new List<ExtraStudent>();
    }

    public List<ExtraStudent> ExStudents { get; set; }

    public void AddPair(Pair pair)
    {
        if (pair is null)
        {
            throw new PairDataException();
        }

        _pairs.Add(pair);
    }

    public void DeletePair(Pair pair)
    {
        if (pair is null)
        {
            throw new PairDataException();
        }

        _pairs.Remove(pair);
    }

    public string GetFaculty() => _faculty;
    public IReadOnlyList<Pair> GetPairs() => _pairs;
}