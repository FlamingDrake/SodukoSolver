namespace SodukoSolver
{
    public interface IPuzzle {
        bool IsSolved();
        bool IsValid();
        IPuzzle Solve();
        void Load(string path);
        void Save(string path); 
    }
}