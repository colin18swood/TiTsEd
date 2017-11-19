using System;
using System.Collections.Generic;

using System.Text;
using TiTsEd.Model;

namespace TiTsEd.ViewModel {
    public sealed class VaginaArrayVM : ArrayVM<VaginaVM> {
        public VaginaArrayVM(CharacterVM character, AmfObject obj)
            : base(obj, x => new VaginaVM(character, x)) {
        }

        protected override AmfObject CreateNewObject() {
            var obj = new AmfObject(AmfTypes.Object);

            obj["type"] = 0;
            obj["hymen"] = true;
            obj["clits"] = 1;
            obj["vaginaColor"] = "pink";

            obj["wetnessRaw"] = 1;
            obj["wetnessMod"] = 0;

            obj["loosenessRaw"] = 1;
            obj["loosenessMod"] = 0;
            obj["minLooseness"] = 1;

            obj["bonusCapacity"] = 0;
            obj["shrinkCounter"] = 0;
            obj["vagooFlags"] = new AmfObject(AmfTypes.Array);

            obj["labiaPierced"] = 0;
            obj["labiaPLong"] = "";
            obj["labiaPShort"] = "";

            obj["clitPierced"] = 0;
            obj["clitPLong"] = "";
            obj["clitPShort"] = "";


            obj["classInstance"] = "classes::VaginaClass";

            return obj;
        }
    }

    public class VaginaVM : ObjectVM {
        public VaginaVM(CharacterVM character, AmfObject obj)
            : base(obj) {
            _character = character;
        }

        private CharacterVM _character { get; set; }

        public int Clits {
            get { return GetInt("clits"); }
            set { SetValue("clits", value); }
        }

        public bool Hymen {
            get { return GetBool("hymen"); }
            set {
                SetValue("hymen", value);
                OnPropertyChanged("Description");
            }
        }

        public double Looseness {
            get { return GetDouble("loosenessRaw"); }
            set { SetValue("loosenessRaw", value); }
        }

        public double LoosenessMod {
            get { return GetDouble("loosenessMod"); }
            set { SetValue("loosenessMod", value); }
        }

        public double MinLooseness {
            get { return GetDouble("minLooseness"); }
            set {
                SetValue("minLooseness", value);
                OnPropertyChanged("Looseness");
            }
        }

        public double Wetness {
            get { return GetDouble("wetnessRaw"); }
            set { SetValue("wetnessRaw", value); }
        }

        public double WetnessMod {
            get { return GetDouble("wetnessMod"); }
            set { SetValue("wetnessMod", value); }
        }

        public double BonusCapacity {
            get { return GetDouble("bonusCapacity"); }
            set { SetValue("bonusCapacity", value); }
        }

        public int ShrinkCounter {
            get { return GetInt("shrinkCounter"); }
            set { SetValue("shrinkCounter", value); }
        }

        public string VaginaColor {
            get { return GetString("vaginaColor"); }
            set {
                SetValue("vaginaColor", value);
                OnPropertyChanged("Description");
            }
        }

        public string[] VaginaColors {
            get {
                return XmlData.Current.Body.SkinTones;
            }
        }

        public int VaginaType {
            get { return GetInt("type"); }
            set {
                SetValue("type", value);
                UpdateFlags(value);
                OnPropertyChanged("Description");
            }
        }

        public void UpdateFlags(int cType)
        {
            List<string> defaultFlags = GetDefaultFlags(cType);

            foreach (FlagItem flag in this.VaginaFlags)
            {
                if (defaultFlags.Contains(flag.ItemName))
                {
                    flag.ItemChecked = true;
                }
                else
                {
                    flag.ItemChecked = false;
                }
            }
            OnPropertyChanged("VaginaFlags");
        }

        public List<string> GetDefaultFlags(int vType)
        {
            List<string> defaultFlags = new List<string>();
            switch (vType)
            {
                case 0: //Human
                case 1: //Equine
                case 3: //Canine
                case 5: //Vulpine 
                case 6: //Bee
                case 10: //Avian
                case 13: //Naga
                case 16: //Gooey
                case 19: //Shark
                case 20: //Suula
                case 24: //Kui-tan
                case 44: //Lapinara
                case 46: //Vanae
                case 49: //Leithan
                case 51: //Synthetic
                case 72: //Swine
                case 74: //Mouthgina
                default:
                    //All of the above have no default flags, so do nothing
                    break;
                case 18: //Gabilani
                case 55: //Nyrea
                    defaultFlags.Add("Lubricated");
                    break;
                case 65: //Gryvain
                    defaultFlags.Add("Nubby");
                    break;
                case 67: //Flower
                    defaultFlags.Add("Aphrodisiac");
                    break;
            }
            return defaultFlags;
        }


        public XmlEnum[] VaginaTypes {
            get {
                return XmlData.Current.Body.VaginaTypes;
            }
        }

        public List<FlagItem> VaginaFlags {
            get { return getFlagList(GetObj("vagooFlags"), XmlData.Current.Body.VaginaFlags); }
        }

        public List<FlagItem> AssFlags {
            get { return getFlagList(GetObj("vagooFlags"), XmlData.Current.Body.AssFlags); }
        }

        public String Description {
            get {
                string output = "a ";
                if (Hymen) {
                    output += "virgin ";
                }

                output += VaginaColor + " ";

                string type = "unknown";
                foreach (var vtype in VaginaTypes) {
                    if (vtype.ID == VaginaType) {
                        type = vtype.Name.ToLower();
                    }
                }

                return output + type + " vagina.";
            }
        }
    }
}
