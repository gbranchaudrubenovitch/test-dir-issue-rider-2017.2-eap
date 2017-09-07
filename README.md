# test-dir-issue-rider-2017.2-eap
Sample to reproduce issue with the NUnit's test directory in Rider 2017.2 EAP 1

## Steps to reproduce
1. open the `sln` in Rider 2017.2 EAP 1
2. restore the `NuGet` packages.
3. build the solution
4. Run all tests.

### Expected results:
* All tests pass.

### Actual results:
* All tests fail.

## Workaround:
* The tests pass in both `Rider 2017.1` and `Visual Studio 2013 + Resharper 2017.2`.
