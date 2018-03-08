using VsixRatingChaser;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace FootieData.Vsix
{
    public class PackageRatingChaser
    {
        public void Hunt(IRatingDetailsDto ratingDetailsDto)
        {
            var extensionDetailsDto = new ExtensionDetailsDto
            {
                AuthorName = Vsix.Author,
                ExtensionName = Vsix.Name,
                MarketPlaceUrl = "https://marketplace.visualstudio.com/items?itemName=GregTrevellick.VSSportsDesk"
            };

            Probe(ratingDetailsDto, extensionDetailsDto);
        }

        public static ChaseOutcome Probe(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto)
        {
            var chaser = new Chaser();
            var chaseOutcome = chaser.Chase(ratingDetailsDto, extensionDetailsDto);
            return chaseOutcome;
        }

    }
}