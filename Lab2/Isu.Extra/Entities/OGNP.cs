using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OGNP
{
    private readonly List<Stream> _streamsList;
    private readonly string _faculty;
    public OGNP(string faculty)
    {
        _faculty = faculty;
        _streamsList = new List<Stream>();
    }

    public List<Stream> GetStreams => _streamsList;

    public void AddStream(Stream stream)
    {
        if (stream is null)
        {
            throw new StreamInvalidException();
        }

        _streamsList.Add(stream);
    }

    public Stream FindStream(Stream stream)
    {
        var empty = new Stream(1000000);
        if (stream is null)
        {
            throw new StreamInvalidException();
        }

        var str = _streamsList.FirstOrDefault(s => Equals(s, stream));
        if (str != null)
            return str;
        return empty;
    }

    public string GetFaculty() => _faculty;
}