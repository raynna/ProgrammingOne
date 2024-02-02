using ProgrammeringOne.course;
using ProgrammeringOne.user;

namespace Programmering_1.course.courses {
    internal class BackPack(User user) : Course(user) {

        private readonly int maxItems = 10;

        public override void Start() {
            WriteLine("Välkommen till ryggsäcken " + user.Username + "!");
            NewLine();
            Option[] options = [
                new(1, "Lägg till något i din ryggsäck", () => AddItem()),
                new(2, "Visa innehållet i din ryggsäck", () => ShowItems()),
                new(3, "Rensa innehållet i din ryggsäck", () => ClearItems()),
                new(8, "Gå tillbaka till huvudmenyn", new Menu(user)),
                new(9, "Avsluta", () => Stop())
                ];
            Option.ShowOptions(options);
            CheckResult();
            Write("Välj: "); 
            char keyPress = ReadKey().KeyChar;
            if (!char.IsDigit(keyPress)) {
                Restart();
                return;
            }
            Option? selectedOption = null;
            foreach (var option in options) {
                if (option.Id.ToString() == keyPress.ToString()) {
                    selectedOption = option;
                    break;
                }
            }
            if (selectedOption == null) {
                Restart();
                return;
            }
            if (selectedOption.Course != null) {
                selectedOption.Course.Start();
                return;
            }
            if (selectedOption.Action != null) {
                selectedOption.Action();
                return;
            }
        }

        public override void Stop() {
            base.Stop();
            return;
        }

        private void ShowItems() {
            if (user.BackPackItems.Count == 0) {
                Result(true, "Du har inga föremål i din ryggsäck!");
                Restart();
                return;
            }
            Clear();
            string response = "Du har " + user.BackPackItems.Count + " föremål i din ryggsäck: ";
            WriteLine(response);
            List<string> list = user.BackPackItems;
            for (int i = 0; i < list.Count; i++) {
                string? item = list[i];
                WriteLine(item.ToString());
            }
            NewLine(1);
            Write("Tryck på valfri tangent för att gå tillbaka till ryggsäcken.");
            ReadKey();
            Restart();
        }

        private void AddItem() {
            if (user.BackPackItems.Count >= maxItems) {
                Result(true, "Din ryggsäck är full.");
                Restart();
                return;
            }
            Clear();
            WriteLine("Här kan du lägga till vad du vill i din ryggsäck.");
            Write("Vad vill du lägga till?: ");
            string? itemToAdd = ReadLine();
            if (string.IsNullOrWhiteSpace(itemToAdd)) {
                AddItem();
                return;
            }
            user.AddItem(itemToAdd);
            user.SaveUser();
            Result(true, "Du har lagt till > " + itemToAdd + " < i din ryggsäck.");
            Restart();
        }

        private void ClearItems() {
            if (user.BackPackItems.Count == 0) {
                Result(true, "Du har inga föremål att rensa i din ryggsäck!");
                Restart();
                return;
            }
            Clear();
            WriteLine("Du är påväg att rensa din ryggsäck.");
            WriteLine("Är du säker att du vill rensa din ryggsäck?");
            Option[] options = {
                new(1, "Ja"),
                new(2, "Nej"),
            };
            NewLine();
            Option.ShowOptions(options);
            NewLine();
            Write("Välj: ");
            char keyPress = Console.ReadKey().KeyChar;
            if (!char.IsDigit(keyPress)) {
                ClearItems();
                return;
            }
            foreach (var option in options) {
                if (option.Id.ToString() == keyPress.ToString()) {
                    if (option.Id == 1) {
                        Clear();
                        user.ClearItems();
                        user.SaveUser();
                        Result(true, "Du har nu rensat din ryggsäck.");
                        Restart();
                    }
                    if (option.Id == 2) {
                        Result(true, "Du valde att inte rensa din ryggsäck.");
                        Restart();
                    }
                }
            }
        }
    }
}
