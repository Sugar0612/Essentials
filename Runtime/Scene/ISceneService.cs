namespace SUG.Essentials
{
    public interface ISceneManagementService
    {
        public string currScene { get; set; }
        public void LoadSceneAsync(string scene);
        public void UnloadSceneAsync(string scene);
    }
}
