using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportingBot_VJaju.Dialogs
{
    public class SurveyDialog : CancelAndHelpDialog
    {
        private const string AgeText = "What is your age?";
        private const string GenderText = "What is your gender?";
        private const string HadPositiveText = "Are you Covid19 positive?";
        private const string HadVaccineText = "Did you had vaccine?";
        private const string OneWordCommentText = "One Word comment about Covid19?";

        public SurveyDialog()
            : base(nameof(SurveyDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                AgeStepAsync,
                GenderStepAsync,
                HadPositiveStepAsync,
                HadVaccineStepAsync,
                OneWordCommentStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> AgeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var surveyDetails = (Covid19ExcelModel)stepContext.Options;

            if (surveyDetails.Age == null)
            {
                var promptOptions = new PromptOptions { Prompt = MessageFactory.Text(AgeText) };
                return await stepContext.PromptAsync(nameof(NumberPrompt<int>), promptOptions, cancellationToken);

            }

            return await stepContext.NextAsync(surveyDetails.Age, cancellationToken);
        }

        private async Task<DialogTurnResult> GenderStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var surveyDetails = (Covid19ExcelModel)stepContext.Options;

            surveyDetails.Age = (int)stepContext.Result;

            if (surveyDetails.Gender == null)
            {

                PromptOptions promptOptions = new PromptOptions
                {
                    Prompt = MessageFactory.Text(GenderText),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(Enum.GetNames(typeof(GenderEnum)).ToList()),
                };

                return await stepContext.PromptAsync(nameof(ChoicePrompt), promptOptions, cancellationToken);
            }

            return await stepContext.NextAsync(surveyDetails.Gender, cancellationToken);
        }
        private async Task<DialogTurnResult> HadPositiveStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var surveyDetails = (Covid19ExcelModel)stepContext.Options;

            surveyDetails.Gender = ((Microsoft.Bot.Builder.Dialogs.Choices.FoundChoice)stepContext.Result).Value;

            if (surveyDetails.HadPositive == null)
            {

                PromptOptions promptOptions = new PromptOptions
                {
                    Prompt = MessageFactory.Text(HadPositiveText),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(Enum.GetNames(typeof(YesNoEnum)).ToList()),
                };

                return await stepContext.PromptAsync(nameof(ChoicePrompt), promptOptions, cancellationToken);
            }

            return await stepContext.NextAsync(surveyDetails.HadPositive, cancellationToken);
        }
        private async Task<DialogTurnResult> HadVaccineStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var surveyDetails = (Covid19ExcelModel)stepContext.Options;

            surveyDetails.HadPositive = ((Microsoft.Bot.Builder.Dialogs.Choices.FoundChoice)stepContext.Result).Value;

            if (surveyDetails.HadVaccine == null)
            {

                PromptOptions promptOptions = new PromptOptions
                {
                    Prompt = MessageFactory.Text(HadVaccineText),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(Enum.GetNames(typeof(YesNoEnum)).ToList()),
                };

                return await stepContext.PromptAsync(nameof(ChoicePrompt), promptOptions, cancellationToken);
            }

            return await stepContext.NextAsync(surveyDetails.HadVaccine, cancellationToken);
        }


        private async Task<DialogTurnResult> OneWordCommentStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var surveyDetails = (Covid19ExcelModel)stepContext.Options;

            surveyDetails.HadVaccine = ((Microsoft.Bot.Builder.Dialogs.Choices.FoundChoice)stepContext.Result).Value;

            if (surveyDetails.OneWordComment == null)
            {
                var promptOptions = new PromptOptions { Prompt = MessageFactory.Text(OneWordCommentText) };
                return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);

            }

            return await stepContext.NextAsync(surveyDetails.OneWordComment, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var surveyDetails = (Covid19ExcelModel)stepContext.Options;

            surveyDetails.OneWordComment = (string)stepContext.Result;
 
            return await stepContext.EndDialogAsync(surveyDetails, cancellationToken);
        }
    }
}
