using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using System.Linq;
using FootieData.Entities.ReferenceData;
using FootieData.Vsix.Helpers;

namespace FootieData.Vsix.Options
{
    public class GeneralOptions : DialogPage
    {
        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionUk1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUk1 { get; set; } = true;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionUk2)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUk2 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionUk3)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUk3 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionUk4)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUk4 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionBr1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInBr1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionDe1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInDe1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionDe2)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInDe2 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionEs1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInEs1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionFr1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInFr1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionFr2)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInFr2 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionIt1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInIt1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionIt2)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInIt2 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionNl1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInNl1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionPt1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInPt1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(LeagueMapping.InternalLeagueCodeDescriptionUefa1)]
        [Description(CommonConstants.InterestedInLeague)]
        public bool InterestedInUefa1 { get; set; } = false;

    }
}
