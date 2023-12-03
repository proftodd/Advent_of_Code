# Advent of Code 2023
https://adventofcode.com/2023

This year it's Typescript. Here are the steps I used to set up the repo.

1. Download Typescript `.gitignore` from Github.
1. Initialize the repo: `npm i typescript --save-dev`
1. Add support for common types: `npm install --save-dev types/node`
1. Set the output directory: edit `tsconfig.json` by uncommenting the outDir line and setting it to `./build`.
1. Add an `index.ts` file.
1. Run the code: `npx tsc; node build/index.js`
1. Add a test package: `npm install --save-dev vitest`; run tests `npm run test`.