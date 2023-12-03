const defaultYear = 2023;

export default function hello(year: number = defaultYear): string {
    return `Welcome to Advent of Code ${year}!`;
}
