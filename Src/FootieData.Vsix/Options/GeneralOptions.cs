using FootieData.Common;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace FootieData.Vsix.Options
{
    public class GeneralOptions : DialogPage
    {
        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionUk1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUk1 { get; set; } = true;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionUk2)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUk2 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionUk3)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUk3 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionUk4)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUk4 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionBr1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInBr1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionDe1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInDe1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionDe2)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInDe2 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionEs1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInEs1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionFr1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInFr1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionFr2)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInFr2 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionIt1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInIt1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionIt2)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInIt2 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionNl1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInNl1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionPt1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInPt1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.InternalLeagueCodeDescriptionUefa1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUefa1 { get; set; } = false;
    }
}
