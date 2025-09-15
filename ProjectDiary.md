I have learned about cold starts. Every redeploy causes api calls to take a long time. So for production sites a premium plan would be better.
Learned about workflows and actions. Skip building app because it's not required. Hopefully removed building of api if no changes are made there.
Working on figuring out logging and KQL Kusto Query Language. Still not sure how to show LogInformation in traces.
Problems with FromBody. Switched to ReadFromJsonAsync and it works. Thinking about "attack" vectors. Added maxlength to input fields. Not sure if needed.
Setting up azure sql server. Trying to figure out differences between EFCore and ms sqlclient and how they work with azure functions and httptriggers.
Creating a service for db connections, finding out ways to use connectionstring. Seems like Configuration isn't used and Environment.GetEnvironmentVariable in the service is the way to go.