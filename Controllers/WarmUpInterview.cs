using Microsoft.AspNetCore.Mvc;
using GrapheneCore.Database;
using GrapheneCore.Entities;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;

namespace GrapheneCore.Controllers;

[ApiController]
[Route("[controller]")]
public class WarmUpController : ControllerBase
{
    public WarmUpController()
    {
        //
    }

    [HttpGet]
    [Route("BinarySearch/{target}")]
    public IResult BinarySearch(int target, [FromQuery] int[] list)
    {
        Array.Sort(list);
        int leftIndex = 0;
        int rightIndex = list.Length - 1;
        // loop
        while (leftIndex <= rightIndex) {
            // use middle
            int middleIndex = (leftIndex + (rightIndex - leftIndex)) / 2;
            int middleValue = list[middleIndex];
            // if it is target return middleIndex
            if (target == middleValue) return Results.Ok(middleIndex);
            // target grater than right middle go right
            if (target > middleValue) leftIndex = middleIndex + 1;
            // target is lesser than middle go left
            else rightIndex = middleIndex - 1;
        }

        return Results.Ok(-1);
    }

    [HttpGet]
    [Route("validate/{bracketString}")]
    public IResult validate(string bracketString)
    {
        System.Console.WriteLine(bracketString);
        var openedBracketStack = new Stack<char>();
        // iterate
        foreach (char character in bracketString) {
            // if it opens add to the stack
            if (ItOpens(character)) {
                openedBracketStack.Push(character);
            }
            // is closing
            else {
                if (openedBracketStack.Count == 0) {
                    return Results.Ok(false);
                }
                // last opened
                var lastOpened = openedBracketStack.Pop();
                if (!Closes(lastOpened, character)) {
                    return Results.Ok(false);
                }
            }
        }
        return Results.Ok(openedBracketStack.Count == 0);
    }

    public bool ItOpens(char brackets) {
        return "{([".Contains(brackets);
    }

    public bool Closes(char opens, char itCloses) {
        if (opens == '{' && itCloses == '}')
            return true;
        if (opens == '[' && itCloses == ']')
            return true;
        if (opens == '(' && itCloses == ')')
            return true;
        return false;
    }

    public bool ItCloses(string brackets) {
        return "}])".Contains(brackets);
    }
}
