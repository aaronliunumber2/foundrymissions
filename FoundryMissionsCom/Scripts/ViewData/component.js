function getComponentById(components, actorID) {
    //lazy  copied this from https://stackoverflow.com/a/19253830
    // iterate over each element in the array
    for (var i = 0; i < components.length; i++) {
        // look for the entry with a matching value
        if (components[i].Number == actorID) {
            // we found it
            return components[i];
        }
    }
}