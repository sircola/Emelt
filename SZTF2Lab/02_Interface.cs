using System;

namespace _02_Interface
{
    #region Interfészek
    interface IMegvehető
    {
        int Ár { get; set; }    // nem lehet: private, protected
        void Megvesz();
    }
    interface IEladható
    {
        int Ár { get; set; }
        void Elad();
    }
    interface IAkciózható : IEladható
    {
        void Akció(double kedvezmény);
    }
    #endregion

    #region Termékek
    public abstract class Termék : IEladható, IMegvehető
    {
        public virtual int Ár { get; set; }     // nem kötelező az absztrakt megjelölés, lehet virtual is!
        public abstract void Elad();            // lehet itt megvalósítása is, akár virtuális is
        public abstract void Megvesz();
    }

    public class NormálTermék : Termék
    {
        public override int Ár { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }  // absztrakt megvalósítása

        public override void Elad() { throw new NotImplementedException(); }

        public override void Megvesz() { throw new NotImplementedException(); }
    }
    public class AkciósTermék : Termék, IAkciózható
    {
        public override int Ár { get; set; }

        public void Akció(double kedvezmény) { throw new NotImplementedException(); }   // interface megvalósítása
        public override void Elad() { throw new NotImplementedException(); }
        public override void Megvesz() { throw new NotImplementedException(); }
    }

    // közvetlenül az interface-ből
    public class TermékA : IMegvehető, IEladható
    {
        public int Ár { get; set; } //implicit

        public void Elad() { throw new NotImplementedException(); }
        public void Megvesz() { throw new NotImplementedException(); }
    }
    public class TermékB : IMegvehető, IEladható
    {
        public int Ár { get; set; } // implicit
        int IEladható.Ár { get; set; }  // explicit

        public void Megvesz() { throw new NotImplementedException(); }

        void IEladható.Elad() { throw new NotImplementedException(); }
    }
    public class TermékC : IMegvehető, IEladható
    {
        public int Ár { get; set; }
        int IEladható.Ár { get; set; }  // explicit
        int IMegvehető.Ár { get; set; } // explicit

        public void Megvesz() { throw new NotImplementedException(); }

        void IEladható.Elad() { throw new NotImplementedException(); }
    }
    #endregion
}
