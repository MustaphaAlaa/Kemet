import { useState, type MouseEventHandler } from 'react';

type props = {
    valueToExpand: string | number,
    onClick: MouseEventHandler<HTMLDivElement>,
    expandedValue: string | number,
    title: string,
    icons: {
        idle: React.ReactNode,
        hover: React.ReactNode,
        expanded: React.ReactNode
    },
    theme?: {
        base?: string; // base background or gradient
        expanded?: string;
        collapsed?: string;
        hover?: string;
        border?: string;
        text?: string;
        rounded?: string;
    };
    children: React.ReactNode
};

export default function OpenClosedLabel({ valueToExpand,
    expandedValue,
    onClick,
    children,
    title,
    icons,
    theme }:
    props) {


    const [isHovered, setHovered] = useState(false);
    const isExpanded = expandedValue === valueToExpand;

    // fallback styles (defaults)
    const baseTheme = {
        base: 'bg-gradient-to-r',
        expanded: 'from-blue-400 via-sky-300 to-sky-200 text-sky-900 font-bold',
        collapsed: 'bg-gray-100 border-3 border-white text-cyan-900 font-bold',
        hover: 'hover:from-emerald-400 hover:via-green-300 hover:to-teal-300 hover:text-lime-800',
        border: isExpanded ? 'rounded-t-lg' : 'rounded-sm',
        text: 'text-2xl',
        rounded: '',
        ...theme, // for overrides
    };


    const labelStyle = `${baseTheme.base} ${isExpanded ? baseTheme.expanded : baseTheme.collapsed} ${baseTheme.hover} ${baseTheme.border} ${baseTheme.text}`;




    return <div
        onMouseEnter={() => setHovered(true)}
        onMouseLeave={() => setHovered(false)}

        className={`flex flex-col cursor-pointer ${baseTheme.rounded}   items-center    text-2xl`} onClick={onClick}>
        <div className={`flex flex-row items-center   ${labelStyle}  w-[100%] p-3 space-x-5`}>
            <span>
                {isExpanded && !isHovered
                    ? icons.expanded
                    : !isExpanded && !isHovered
                        ? icons.idle
                        : icons.hover}
            </span>
            <p>
                {title}
            </p>
        </div>

        {isExpanded && children}

    </div>
}
