import { HiOutlineArrowSmLeft, HiOutlineArrowSmRight } from "react-icons/hi";
import { HiOutlineArrowSmallRight } from "react-icons/hi2";

interface IPagination {
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
    hasNext: boolean;
    hasPrevious: boolean;
}


const getPageNumbers = (currentPage: number, totalPages: number) => {
    const delta = 2; // to show 2 pages after or before current page;

    //  hold the middle page numbers (surrounding current page).
    // if current page 5 it'll be [3,4,5,6,7]
    const range = [];
    const rangeWithDots = [];

    const rangeStartAt = Math.max(2, currentPage - delta);
    const rangeEndsAt = Math.min(totalPages - 1, currentPage + delta);

    for (let i = rangeStartAt; i <= rangeEndsAt; i++)
        range.push(i);

    if (currentPage - delta > 2) // to see if dots will be added or not
        rangeWithDots.push(1, '...');
    else
        rangeWithDots.push(1);

    rangeWithDots.push(...range);

    if (currentPage + delta < totalPages - 1)
        rangeWithDots.push('...', totalPages);
    else
        rangeWithDots.push(totalPages);

    return rangeWithDots;


}


export default function Pagination({
    currentPage,
    totalPages,
    onPageChange,
    hasNext,
    hasPrevious,
}: IPagination) {
    if(totalPages <= 1) return null;

    return (
        <div dir="ltr" className="flex items-center justify-center space-x-2 mt-6">
            <button
                onClick={() => onPageChange(currentPage - 1)}
                disabled={!hasPrevious}
                className="px-3 py-2 text-sm font-medium text-gray-500 bg-white border border-gray-300 rounded-md hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
            >
                <HiOutlineArrowSmLeft className="w-4 h-4" />
            </button>

            {getPageNumbers(currentPage, totalPages).map((page, index) => (
                <button
                    key={index}
                    onClick={() => typeof page === 'number' && onPageChange(page)}
                    disabled={page === '...'}
                    className={`px-3 py-2 text-sm font-medium rounded-md ${page === currentPage
                        ? 'text-white bg-blue-600 border border-blue-600'
                        : page === '...'
                            ? 'text-gray-500 cursor-default'
                            : 'text-gray-500 bg-white border border-gray-300 hover:bg-gray-50'
                        }`}
                >
                    {page}
                </button>
            ))}


        
            <button
                onClick={() => onPageChange(currentPage + 1)}
                disabled={!hasNext}
                className="px-3 py-2 text-sm font-medium text-gray-500 bg-white border border-gray-300 rounded-md hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
            >
                <HiOutlineArrowSmallRight className="w-4 h-4" />
            </button>
        </div>
    );
}
