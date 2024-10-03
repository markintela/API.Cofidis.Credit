import Image from "next/image";
import CreditConsultForm from './components/CreditConsultForm';
import logo from './img/logo.png';

export default function Home() {
  return (
    <div className="grid grid-rows-[20px_1fr_20px] h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]">
      <main className="flex flex-col items-center justify-center min-h-screen">
        <div className="flex flex-col space-y-8 mx-auto">
          <Image
            src={logo}
            alt="Logo da minha aplicação"
            className="w-48 h-48 mx-auto" // Set width and height with Tailwind classes
          />
          <CreditConsultForm />
        </div>
      </main>
      <footer className="row-start-3 flex gap-6 flex-wrap items-center justify-center">
        {/* Rest of your footer content */}
      </footer>
    </div>
  );
}
