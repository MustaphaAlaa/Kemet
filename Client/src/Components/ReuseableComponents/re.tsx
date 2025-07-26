// components/ResponsiveSidebar.tsx
import { useState, useEffect, type ReactNode } from 'react';

type ResponsiveSidebarProps = {
  children: ReactNode;
  breakpoint?: number; // Optional custom breakpoint
  sidebarContent: ReactNode;
  sidebarWidth?: string; // Tailwind width class
  mobileMenuIcon?: ReactNode; // Custom hamburger icon
  overlayClass?: string; // Custom overlay classes
  sidebarClass?: string; // Custom sidebar classes
};

export const ResponsiveSidebar = ({
  children,
  breakpoint = 768,
  sidebarContent,
  sidebarWidth = 'w-64',
  mobileMenuIcon,
  overlayClass = 'bg-black bg-opacity-50',
  sidebarClass = 'bg-gray-800 text-white'
}: ResponsiveSidebarProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const [isMobile, setIsMobile] = useState(false);

  useEffect(() => {
    const checkScreenSize = () => {
      setIsMobile(window.innerWidth < breakpoint);
    };

    checkScreenSize();
    window.addEventListener('resize', checkScreenSize);
    return () => window.removeEventListener('resize', checkScreenSize);
  }, [breakpoint]);

  const toggleMenu = () => setIsOpen(!isOpen);

  return (
    <div className="flex">
      {/* Mobile menu button */}
      {isMobile && (
        <button
          onClick={toggleMenu}
          className="fixed top-4 left-4 z-50 p-2 rounded-md text-gray-700 hover:text-gray-900 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-indigo-500"
          aria-label="Toggle menu"
        >
          {mobileMenuIcon || (
            <svg
              className="h-6 w-6"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M4 6h16M4 12h16M4 18h16"
              />
            </svg>
          )}
        </button>
      )}

      {/* Sidebar */}
      <div
        className={`${isMobile ? 'fixed inset-y-0 left-0 transform' : 'relative'} 
          ${(isOpen || !isMobile) ? 'translate-x-0' : '-translate-x-full'}
          ${sidebarWidth} ${sidebarClass} transition-transform duration-300 ease-in-out z-40`}
      >
        {sidebarContent}
      </div>

      {/* Overlay */}
      {isMobile && isOpen && (
        <div
          className={`fixed inset-0 z-30 ${overlayClass}`}
          onClick={toggleMenu}
        />
      )}

      {/* Main content */}
      <div className={`flex-1 ${!isMobile && isOpen ? `ml-${sidebarWidth.split('-')[1]}` : ''}`}>
        {children}
      </div>
    </div>
  );
};