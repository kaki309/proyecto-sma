# Repository Management Guidelines

## Repository Management

**GitHub Desktop** is established as the tool to be used for managing the connection with the repository. All content included in the repository (Source code, documentation, commits, etc.) will be managed in **English**.

Each developer, when starting a work session, must:

1. Access the GitHub Desktop tool
2. Ensure there are no pending changes to download from the repository before making any modifications to the project

At the end of the work session, developers must:

1. Upload to the repository all commits generated during the session, ensuring it is updated with the latest changes worked on
2. Optionally, upload to the repository any changes being made at any moment within the work session if deemed necessary

Access the project repository via the following link: [Public Repository](https://github.com/kaki309/proyecto-sma)

---

## Branch Management

### Creating Branches

- All developers can create new branches, implementing a new one for each task to be worked on
- The branch name must be descriptive in relation to the task content
- The branch name must include as a prefix the User Story code in the following format: `[HU X.Y.Z] Branch name`

### Pull Requests

- All developers can create pull requests for branches they are working on, whether to merge them into other branches or to integrate content from other branches
- The **team leader** is responsible for validating and accepting each pull request

### Main Branch

- The main branch (`Main`) is reserved for exclusive use by the team leader or any other team member with prior authorization from the leader

---

## Commit Management

Commits should be made as promptly as possible, ensuring they do not encompass multiple types of alterations simultaneously.

The following format must be used:

### Commit Title

Add the prefix corresponding to the type of alteration being made. The prefixes to be used are:

- **[Add]**: New content is being added
- **[Fix]**: Correcting an error from a previous [Add] commit
- **[Patch]**: Modifying existing content
- **[Working]**: Work in progress. This commit saves general changes. Should be used only when a single prefix cannot be applied to a commit that encompasses multiple alterations. Use sparingly
- **[Core]**: Adding or modifying core project content (Dependencies, configuration, etc.). **These changes are made exclusively in the `Main` branch and are the responsibility of the team leader or any other team member with prior authorization from the leader**
- **[Docs]**: Modifying project or repository documentation. **These changes are made exclusively in the `Main` branch and are the responsibility of the team leader or any other team member with prior authorization from the leader**

### Commit Description

- Write a brief summary of the commit content
- Include a list of modifications if necessary
