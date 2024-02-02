
using ProgrammeringOne.user;

namespace ProgrammeringOne.course {
    internal class Course(User user) {

        internal readonly User User = user;

        internal bool showResult = false;
        internal string resultText = "";

        internal class Option {
            public int Id { get; }
            public string Description { get; }
            public Action? Action { get; }
            public Course? Course { get; }

            public Option(int id, string description, Action? action, Course? course) {
                Id = id;
                Description = description;
                Action = action;
                Course = course;
            }

            public Option(int id, string description, Action action)
                : this(id, description, action, null) { }

            public Option(int id, string description, Course course)
                : this(id, description, null, course) { }

            public Option(int id, string description)
                : this(id, description, null, null) { }

            public static void ShowOptions(Option[] options) {
                foreach (var option in options) {
                    Console.WriteLine($"[{option.Id}] {option.Description}");
                }
            }
        }


        public void Result(bool show, string text) {
            this.showResult = show;
            this.resultText = text;
        }

        public void CheckResult(string title = "") {
            if (!showResult) {
                if (!string.IsNullOrEmpty(title)) {
                    WriteLine(title);
                }
                return;
            }
            NewLine();
            WriteLine("[Svar]: " + resultText);
            NewLine();
            this.resultText = "";
            this.showResult = false;
        }

        public static void NewLine(int lines) {
            for (int i = 0; i < lines; i++) {
                Console.WriteLine("");
            }
        }

        public static void NewLine() {
            Console.WriteLine("");
        }

        public static void Clear() {
            Console.Clear();
        }

        public static void WriteLine(string text) {
            Console.WriteLine(text);
        }


        public static void Write(string text) {
            Console.Write(text);
        }

        public static string? ReadLine() {
            return Console.ReadLine();
        }

        public static ConsoleKeyInfo ReadKey() {
            return Console.ReadKey();
        }

        public virtual void Start() {
            //start override
        }

        public virtual void Restart() {
            Clear();
            Start();
        }

        public virtual void Stop() {
            Clear();
            WriteLine("Du har valt att stoppa programmet.");
            Write("Tryck på valfri tangent för att fortsätta.");
            ReadKey();
            //TODO STOP PROGRAM FULLY
        }

        public virtual void Launch(Course course) {
            Clear();
            course.Start();
        }
    }
}
