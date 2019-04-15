export enum SortType {
    Asc,
    Desc 
}

export function ascendingDateSorter<T>(dateAccessor: (x: T) => Date) {
    return (a: T, b: T) => {
        return sortAsc(dateAccessor.call(a), dateAccessor.call(b))
    }
}

export function descendingDateSorter<T>(dateAccessor: (x: T) => Date) {
    return (a: T, b: T) => {
        return sortDesc(dateAccessor.call(a), dateAccessor.call(b))
    }
}

function sortAsc(a: Date, b: Date): number {
    return a.getTime() - b.getTime();
}
function sortDesc(a: Date, b: Date): number {
    return b.getTime() - a.getTime();
}