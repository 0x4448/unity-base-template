# unity-base-template
A base template for making Unity games.

## Local repository setup
1. Install the [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks) or newer.
2. `cd` into your local repo and run `git lfs install` to setup [Git LFS](https://git-lfs.github.com).
3. Add these lines to your pre-commit git hook (`.git/hooks/pre-commit`):
```
#!/bin/sh

solution=$(find . -maxdepth 1 -name '*.sln' -type f | head -1)
changed=$(git diff --cached --diff-filter=ACM --name-only -z "*.cs" | xargs -0)
if [[ -n "$changed" ]]; then
  dotnet format "$solution" --verify-no-changes --include $changed
  exit $?
fi
```
4. Setup Unity's merge tool in your local repo git config (`.git/config`):
```
[merge]
	tool = unityyamlmerge
[mergetool "unityyamlmerge"]
	trustExitCode = false
	# Replace with your editor's installation path
	cmd = 'C:\\Program Files\\Unity\\Hub\\Editor\\UNITY_VERSION\\Editor\\Data\\Tools\\UnityYAMLMerge.exe' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
```