# Bugs

- in portal => createColor component in phone responsive design when i shrink the view the design are going crazy.
- in CreateProduct => user will select colors for the product only those colors should be appear when user select that allColorsHasDifferentSizes (fixed);

- if navigate a route direct from address bar it'll throw an error that properties are null, this route to work ordinary it should be navigate from the app not the address bar, this bug because this route depends on outer components that should be navigated first
