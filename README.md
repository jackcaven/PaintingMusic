# Painting Music

![PM Logo](docs/images/pm_logo.png)

## Description

Painting Music is a project that looks to use explainable AI that converts the process of painting a picture into music.

## Repository Structure

We try to follow clean architecture as best to our ability.  Within the C# components in this repo, there is a clear architectural design to help reduce code duplication and increase extensibility.

### Key Layers

#### Core

[Core](/src/Shared/Core/) is, as the name states, the core of all our functionality.  This is where our key interfaces, and datastructures exist.  At this level there should be no external dependencies, as all our solutions should depend on it.

#### Infrastructure

[Infrastructure](/src/Shared/Infrastructure/) is the where we keep the tools that can be built using Core.  This layer is then used by our applications to carry out what they need.  This layer will depend on the Core layer of our architecture. Currently we have two solutions in this layer:

- [Music Generation](/src/Shared/Infrastructure/MusicGeneration/)
  - This is where all the music generation occurs (e.g. Markov Music Generation)
- [Utitlities](/src/Shared/Infrastructure/Utilities/)
  - This is where all generic tools that can be useful in multiple applications are kept.

#### Applications

Using Painting Music technology, two different applications have been developed:

- [Canvas Capture](/src/Applications/CanvasCapture/)
- [Sketchpad](/src/Applications/SketchpadServer/)

Another application in this repository that is required to be used alongside Canvas Capture is:

- [Music Player](/src/Applications/MusicPlayer/)
  - Music Player does not depend on any of the infrastructure or core layers, as it is written in Python.

These applications and their respective documentation can be found in the application directory.

## Contributing

### Cloning

You are able to clone the full repository by any means.  We personally recommend using the following command:

```bash
git clone 'url'
```

### Trunk-Based Development

We follow Trunk-Based Development (TBD) to ensure continuous integration and minimize the risk of large merge conflicts. In this approach:

1. Work directly on the `main` branch is avoided to maintain a stable codebase.
2. Developers are encouraged to create a new branch for each feature, bug fix, or task.
3. Once the task or feature is complete, a Pull Request (PR) should be opened to merge the branch back into main.

#### Workflow

1. Create a branch from main (use descriptive names for branches):

    ```bash
    git checkout -b feature/your-feature-name
    ```

2. Develop the feature or fix the issue on your branch.
3. Test your changes thoroughly.
4. Create a PR against main and provide a detailed description of your changes. Be sure to:
    - Include relevant information or context for reviewers.
    - Link to any related issues (if applicable).
5. The PR will be reviewed by a team member. Once approved, it will be merged into main.

#### Why Trunk-Based Development?

- Frequent integration: Developers integrate their changes frequently, which helps to identify issues earlier.
- Reduced merge conflicts: With smaller, more frequent changes, the likelihood of conflicts decreases.
- Improved code quality: Code reviews help ensure that the code meets the project's standards before merging.

### Commit Convention

Developing in Painting Music, it is asked that commits to the main branch follow conventional commits. See [here](https://gist.github.com/qoomon/5dfcdf8eec66a051ecd85625518cfd13) for further documentation on conventional commits.

#### Commit Examples and explanation

```powershell
feat(canvas-capture): add some work
refactor(sketchpad): change directory structure
fix(core): adjust parameters to resolve issue
docs(markov): add good commit convention section
chore(sketchpad): alter package description
```

Key thing to takeaway from the above examples is:

1. The key word at the start:

   | Type      | Description                                                                 |
   |-----------|-----------------------------------------------------------------------------|
   | feat      | This is when you are adding a new feature (or contributing towards a feature)|
   | refactor  | This is when you have changed the way code is written to improve scalability/readability etc |
   | fix       | When you have fixed a bug                                                   |
   | docs      | When you have added to the docs                                              |
   | chore     | This is for work that has been added outside the above categories            |
  
2. Name of the model.  This is essentially the model you are working e.g. Markov/Markov2/Mercury
3. Using imperative language and keeping commit messages short and consise (use words like add, change, alter, etc)
