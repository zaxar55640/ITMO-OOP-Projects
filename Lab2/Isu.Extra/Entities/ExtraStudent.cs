using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class ExtraStudent : Student
{
    private const int _maxSubjectCount = 2;
    private List<OGNP> _ognpList;
    private GroupWithFaculty _group;

    public ExtraStudent(string name, GroupWithFaculty group, int id)
        : base(name, group, id)
    {
        _group = group;
        _ognpList = new List<OGNP>();
    }

    public void EnrollmentOnOgnp(OGNP ognp, Stream stream)
    {
        if (ognp.GetFaculty() == _group.GetFaculty())
            throw new NotAvailableOGNPException();
        if (_ognpList.Contains(ognp)) throw new StudentAlreadyEnrolled();
        if (_ognpList.Count == _maxSubjectCount)
            throw new MaxOgnpPickedException();

        _ognpList.Add(ognp);
        ognp.FindStream(stream).AddStudent(this);
    }

    public void UnEnrollmentOnOgnp(OGNP ognp)
    {
        if (!_ognpList.Contains(ognp))
        {
            throw new StudentAlreadyEnrolled();
        }

        _ognpList.Remove(ognp);
    }

    public GroupWithFaculty GetGroup() => _group;
    public List<OGNP> GetOGNPs() => _ognpList;
    public string GetName() => Name;
}