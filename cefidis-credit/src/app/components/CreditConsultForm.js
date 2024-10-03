'use client'; // Esse comando é necessário no Next.js para habilitar componentes interativos no front-end.

import React, { useState } from 'react';

export default function CreditConsultForm({ onSubmit }) {
  const [nif, setNif] = useState('');
  const [baseSalary, setBaseSalary] = useState('');
  const [error, setError] = useState(null);
  const [creditApproval, setCreditApproval] = useState(null); // Estado para armazenar o resultado da consulta

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);
    setCreditApproval(null); // Limpar o estado anterior

    // Validação simples
    if (!nif || !baseSalary) {
      setError('Por favor, preencha todos os campos.');
      return;
    }

    try {
      // Definindo a URL base da API
      const apiUrl = 'https://localhost:7011/credit/GrantingCredit';

      // Criando o endpoint com os parâmetros
      const response = await fetch(`${apiUrl}?nif=${nif}&baseSalary=${baseSalary}`, {
        method: 'GET', // Método GET para consulta
        headers: {
          'Access-Control-Allow-Headers': 'Content-Type',
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          'Access-Control-Allow-Methods': 'OPTIONS,POST,GET,PATCH',
        },
      });

      // Verifica se a resposta foi bem sucedida
      if (!response.ok) {
        throw new Error('Erro ao processar sua solicitação.');
      }

      // Obtém os dados da resposta em formato JSON
      const data = await response.json();

      // Armazena o resultado da consulta no estado
      setCreditApproval(data.isAbleToCredit);

      // Chama o callback onSubmit com o resultado da consulta (se aprovado ou não)
      //onSubmit(data.isAbleToCredit);

    } catch (err) {
      // Define a mensagem de erro em caso de falha
      setError('Erro de conexão. Tente novamente mais tarde.');
    }
  };

  return (
    <div className="bg-gray-100 p-8 rounded-lg shadow-lg max-w-md mx-auto">
      <h2 className="text-2xl font-bold text-center text-gray-800 mb-6">Consulta de Crédito</h2>
      <form onSubmit={handleSubmit} className="space-y-6">
        <div className="flex flex-col">
          <label htmlFor="nif" className="text-sm font-medium text-gray-700">NIF</label>
          <input
            type="text"
            id="nif"
            name="nif"
            value={nif}
            onChange={(e) => setNif(e.target.value)}
            className="mt-1 p-2 block w-full border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500"
            placeholder="Digite o NIF"
          />
        </div>

        <div className="flex flex-col">
          <label htmlFor="baseSalary" className="text-sm font-medium text-gray-700">Salário Base</label>
          <input
            type="number"
            id="baseSalary"
            name="baseSalary"
            value={baseSalary}
            onChange={(e) => setBaseSalary(e.target.value)}
            className="mt-1 p-2 block w-full border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500"
            placeholder="Digite o salário base"
          />
        </div>

        <button
          type="submit"
          className="w-full py-2 px-4 bg-blue-600 text-white font-semibold rounded-md shadow hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
        >
          Consultar
        </button>

        {error && (
          <div className="text-red-500 text-sm mt-4">
            <p>{error}</p>
          </div>
        )}

        {/* Exibe o resultado da consulta */}
        {creditApproval !== null && (
          <div className="text-green-500 text-sm mt-4">
            <p>{creditApproval ? 'Crédito aprovado!' : 'Crédito não aprovado.'}</p>
          </div>
        )}
      </form>
    </div>
  );
}
