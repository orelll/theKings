// See https://aka.ms/new-console-template for more information

using TheMonarchs;


var token = new CancellationToken();
var questionsSolver = new QuestionsSolver();
await questionsSolver.AnswerQuestion1(token);
await questionsSolver.AnswerQuestion2(token);
await questionsSolver.AnswerQuestion3(token);
await questionsSolver.AnswerQuestion4(token);

Console.ReadKey();