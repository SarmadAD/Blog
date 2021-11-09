using Blog.Models;

namespace Blog.ViewModel
{
    public class PostEditViewModel
    {
        public Post SelectedPost { get; set; }  = new Post();
        public bool IsLoading { get; set; }
    }
}
