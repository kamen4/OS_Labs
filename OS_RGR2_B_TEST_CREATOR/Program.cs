using System.Text;

namespace OS_RGR2_B_TEST_CREATOR;

internal class Program
{
    static Random rnd = new();
    static string alp = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static string Permute(string str)
    {
        var arr = str.ToCharArray();
        for (int i = 0; i < str.Length; i++)
        {
            int idx = rnd.Next(str.Length);
            (arr[i], arr[idx]) = (arr[idx], arr[i]);
        }
        return string.Concat(arr);
    }

    private static string Exchange(string str)
    {
        Dictionary<char, char> map = new();
        foreach (char c in alp.ToCharArray())
            map.Add(c, c);
        for (char i = 'A'; i <= 'Z'; i++)
        {
            char idx = (char)('A' + rnd.Next(str.Length));
            (map[i], map[idx]) = (map[idx], map[i]);
        }
        StringBuilder sb = new();
        foreach (char c in str.ToCharArray())
            sb.Append(map[c]);
        return sb.ToString();
    }

    static void Main(string[] args)
    {
        string words = "teeth tell describe person inquisitive light swim locket root houses kind metal knit download realize first flashy divergent hurried street death nation volleyball loving addition puzzling tired milk superficial brush silver sparkling judge shun itchy pay grade rural grandfather refuse free admit butter operation verify puny trust card waves swing poison zoo notebook light attempt harm riddle envious scream friendly machine outstanding find handy books burly mew encourage start gainful crack rid recite circle overt perpetual rain blossom make soda drive brash writing noxious contain breath discovery fax bet position wren lead indulge regret key lumpy astonish lawyer flesh didactic cats save amount faucet unhealthy monkey awake wellmade wonderful tend wasteful light youthful ordinary squirrel nice admire place laborer teeny idealize accept acceptable basketball arch cloth low upset splendid lackadaisical moan own consecrat inflate need sew acoustics initiate imperil vast lethal spotless comparison brush create illegal shaggy plastic cover fight heartbreaking signal sloppy soup small abide instrument painful sour breed locket rampant outstanding important purring self married stingy promise wet test nasty close macho glow end merge spin witty effect ugliest fairies spoon scrawl kid elegant aspiring conquer stretch horse tub want acrid leg grow square typical permit ring seat plan rub believe nutritious vanish roar table nervous berry hurl create nebulous intelligent finicky sell boats late rebel uneven magical complex incompetent ceaseless beautify care peaceful play automatic sashay existence receipt bray rightful dock banana transport gun neck inhale tub selfish glamorous general sneak contradict worthless zinc like obtainable advertisement rose exchange position uproot minor troubled middle boorish level engrave scrawny offbeat muddled bite inaugurate moaning tear bashful earthquake charming suggest ghost splendid admit crib hollow drink calculator dress ludicrous hellish cheese afternoon kid wind zoo parallel satisfying inhale acidic wind convert animated thunder flimsy prose key locket gainful regret laborer bad thick sashay suppose physical handy previous cork garrulous obnoxious park choke persuade respect grass evanescent persuade quarrelsome cuddly material gorgeous charming ratty sour therapeutic knife cultured approve steady alight  remake voracious bit womanly punishment rush corn animated thin bun stoop art weep incredible address ugliest queen harmonious shiny calculate scat low expansion ponder moo inexpensive implant glitter nerve ordinary brash glue cease mixed unused mitten separate inquire lovely prose brother parsimonious tawdry foolish glorious ultra fretful salt observe minute impinge console know scowl watch limping glamorous phone marvelous instinctive question stride view subsequent transfer sister quilt sad solve advise hideous amusing example mark tedious men collect super silent market lawyer brain coordinated orde scarp enlighten saunter friend waste needle minister bawdy crime impress hurl basket install faint bomb pacify band K cherry sweat legal damage previous plan ignore tidy godly bat flag imperil cobweb crazy meeting hunt humiliate horrify separate weather craven reduce melted skin attack magical root seed suggest treasure contradict ground distribution disagreeable substance different bread sanction actor brain repeat brush cake proud exclude phone crush tread known uppity digestion axiomatic wake declare inculcate needle bun thing kiss underwear banana visitor amusement sanctify clutch inflame proud border hypnotize secretive worry expansion bewildered plant rebel ancient slay horses feet minister male joyous observe route young youthful amused illustrate shut needle hen hope say ants harbor false quirky increase maid put sack tread draconian keep decide gruesome inject solicit industrious theory check good recollect hands comment enlarge knife shame fluttering mitten control plan symptomatic wrench buy evaporate consort umbrella wholesale month expert holistic gentle legs event catch owe poised wellgroomed relate crib violent tray slink fling shirt massive banana ray vein succinct wooden oranges club weep husky home erase remain betray church snails fuzzy squealing ball boorish breakfast land sleet canvas profuse glorious jittery irate govern motionless cause work value recall stare profit thump lade nonchalant conclude train renew pointless zipper pricey industry jump lizards guttural merge trousers journey awesome pail dead languid curve counsel wistful lumber operation moaning uproot play reminiscent sanctify rest consult comment ignite ignore clammy normal rigid vessel vary wrong gratis rely partner blueeyed obey pale lean smile root tall tasty sun doubtful argument pardon utter place yam high purple faint taste betray lush finish back fertile tedious brothers convince beast cast detach indulge thank lead wealth survive board apple point dramatic basketball giraffe fail picayune stupid do download rose practise far fretful lose grip edge cure quizzical milk triumph saturate graceful scale acid curved lettuce abhorrent real reach measure lackadaisical premium feed wellgroomed swell send ethereal study calendar ingest vague chide bat idolize sable downtown fetch chief cross observe gaping study carry young husky welltodo classify moan moon ticket pump gun signify rotten psychedelic bit move control damp toothpaste rake boundless illtreat boast polite pointless balance old sell fold strive noise mash melodic button houses shaggy unsightly full welltodo fascinated lace thundering chief stimulate wary cook fit saturate shoe homeless force explain dare mind accidental illumine encourage trust middle leaf toothbrush youthful brush clothe geese contrive broken crow stem curvy delete yawn sturdy descriptive sincere parched true sky incredible courageous morning horrify flower smoke obeisant hit brothers diminish meaty notebook matter zippy remove stick agreeable silent daughter hear educated consecrat innate birds exciting frame preach lewd waves hideous art oranges crush kindhearted silent goofy excited sow unwieldy melted note unknown cheap careless quack chivalrous crave stretch tree consider reminiscent hurt drink handsomely early plant start stress sad saddle decrease sudden wistful scarecrow grape stretch innate ring round tax tan certain approve maintain sanctify knowledge space wise lend minute omniscient malicious plain kettle girl representative jolly caring thankful jazzy charge print use loss hose threatening implore damaging cagey uptight pray cent prevent canvas maintain mouth bloody resonant shun cooperate eyes achiever beef massive ear fan maid implicate wrathful trade detect rice mow seemly typical sting overconfident paltry zephyr selection imaginary assert copy become drunk roomy gratis impartial lonely kind apologize cow tacit baby talk memory suggest chip diligent succinct delirious rise thick adjustment slip brake zebra ashamed invite celebrate idealize kindhearted gamy lamp impress damaged lip hate pay healthy include swanky touch clever";
        string[] wordsArr = (words + " " + words).Split(' ');
        int widx = 0;
        string path = "C:\\Users\\volde\\Desktop\\TestCases";

        for (int i = 1; i <= 300; i++)
        {
            int wordsCount = rnd.Next(7);
            List<string> strs = new();
            for (int j = 0; j < wordsCount; ++j)
            {
                if (string.IsNullOrEmpty(wordsArr[++widx]))
                {
                    j--;
                    continue;
                }
                strs.Add(wordsArr[widx].ToUpper());
            }
            List<string> strsCoded = new();
            List<string> answers = new();
            foreach (var str in strs)
            {
                string res = str;
                if ((rnd.Next(2) & 1) == 0)
                {
                    answers.Add("NO");
                    char to = 'A';
                    for (int cidx = 1; cidx < res.Length; cidx++)
                        if (res[cidx] != res[0])
                        {
                            to = res[cidx];
                            break;
                        }

                    StringBuilder sb = new();
                    for (int j = 0; j < res.Length; j++)
                    {
                        if (res[j] == to)
                            sb.Append(res[0]);
                        else
                            sb.Append(res[j]);
                    }
                    res = sb.ToString();
                }
                else
                    answers.Add("YES");
                if ((rnd.Next(2) & 1) == 0)
                    res = Permute(res);
                if ((rnd.Next(2) & 1) == 0)
                    res = Exchange(res);
                strsCoded.Add(res);
            }

            using (StreamWriter SWout = new($"{path}\\Test {i}.OUT", false, Encoding.UTF8))
            using (StreamWriter SWin = new($"{path}\\Test {i}.IN", false, Encoding.UTF8))
            {
                SWin.WriteLine(wordsCount);
                for(int j = 0; j < wordsCount; ++j)
                {
                    SWin.WriteLine(strs[j]);
                    SWin.WriteLine(strsCoded[j]);
                    SWout.WriteLine(answers[j]);
                }
            }
        }
    }   
}