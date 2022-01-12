using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services
{

    public abstract class User<TKey>
        where TKey: IEquatable<TKey>
    {
        public abstract TKey Key { get; set; }
    }



    public interface INumberCreator<TNum>
        where TNum: IEquatable<TNum>, IComparable<TNum>
    {
        TNum GetRandomNum();
    }

    public interface IDoubleNumberCreator<TNum>
        where TNum : IEquatable<TNum>, IComparable<TNum>
    {
        (TNum tnum1, TNum tnum2) GetNumTuple();
    }




    public class IntCreator: INumberCreator<int>
    {
        public int GetRandomNum()
        {
            return 0;
        }
    }

    public class DoubleCreator : IDoubleNumberCreator<int>
    {
        public (int tnum1, int tnum2) GetNumTuple()
        {
            return (0, 0);
        }
    }

    public abstract class NumCreator<TNum>
    {
        public abstract TNum GetRandomNum();
    }
    public abstract class DoubleCreatorAbs<TNum>
    {
        public abstract (TNum tnum1, TNum tnum2) GetNumTuple();
    }

    public class IntCreatorAbs : NumCreator<int>
    {
        public override int GetRandomNum() => 0;
    }
    
    public class DubCreatorAbs : DoubleCreatorAbs<int>
    {
        public override (int tnum1, int tnum2) GetNumTuple() => (0, 0);
    }

    public class CoolCreator : INumberCreator<int>, IDoubleNumberCreator<int>
    {
        public (int tnum1, int tnum2) GetNumTuple()
        {
            return (0, 0);
        }

        public int GetRandomNum()
        {
            return 0;
        }
    }


    public interface ISampleUploadService
    {
        public void UploadSample(FileStream fs);
        public FileStream DownloadSample(string sampleName);
    }

    

}
