## ML.Net Language Detector Sample

### This is a simple multi-classification example made by ML.NET framework.

This project offer a ML.NET model that detect from a text input what language is it.
The project contains unit tests, a console example, and a web project made with Blazor Server. The idea is
in principle very simple, in this example a multi-classification algorithm is used to indicate to the model,
how to associate a certain order of words and the writing of words themselves to a language.
The algorithm that was decided to use is 'NaiveBayes' which, among other things, offers for the data set used
impressive performance and precision.
The data set for training is not ideal at all, but very good results were achieved in the end
the use of multi-classification. The ideal is to pass to the algorithm a load of data much more notable and well
organized in order to supervise the training even more accurately.