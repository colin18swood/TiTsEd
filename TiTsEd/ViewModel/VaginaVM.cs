﻿using System;
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

            obj["fullness"] = 0;

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

        public double Fullness {
            get { return GetDouble("fullness"); }
            set { SetValue("fullness", value); }
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
                OnPropertyChanged("Description");
            }
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
