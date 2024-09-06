Jeg har lavet to pakker i løsningen. En lille en, der kun indeholder selve den delmængde af Dalux API'et, der er brug for til at uploade filer, og så selve console applikationen, hvor jeg anvender API'et til at uploade en fil.

Jeg har forsøgt at genbruge dine wrappers til `Project` og `File`, da jeg tænker, det er dem, du får dine input fra. I eksemplet har jeg bare hardcodet noget, der peger ned på en konkret fil på mit eget filsystem, ligesom API-key og URL til Dalux bare er noget krims-krams. Jeg håber, det er tydeligt, hvad du skal rette i `Program` for at kunne starte det op lokalt.

### Interessante Klasser

- **FileUploader**: Denne klasse har to måder, du kan uploade en fil på:
  - Sekventielt, hvor vi uploader et chunk ad gangen.
  - Parallelt, hvor vi samler alle chunks op og fyrer dem afsted asynkront (en af de fede ting ved async). Det sidste burde virke, hvis Dalux har lavet API'et rigtigt, men som sagt, så er det ikke noget, jeg har testet.

- **IFileUploadClient**: Dette er et eksempel på, hvordan du kan lave en REST-klient ved hjælp af [RestEase](https://github.com/canton7/RestEase/blob/master/README.md), som jeg selv er ret begejstret for. Med RestEase kan du lave dine klienter helt uden at få fedtet koden ind i HTTP- og JSON-gymnastik – det klarer frameworket for dig.
