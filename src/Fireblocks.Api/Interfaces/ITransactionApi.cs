using Fireblocks.Api.Models.Requests;
using Fireblocks.Api.Models.Requests.Params;
using Fireblocks.Api.Models.Responses;
using Refit;

namespace Fireblocks.Api.Interfaces;

public interface ITransactionApi : IFireblocksApi
{
	[Get("")]
	Task<ApiResponse<IEnumerable<TransactionDetailsModel>>> GetTransactionsAsync(
		[Query] ListTransactionsParams queryParams);

	[Post("")]
	Task<ApiResponse<CreateTransactionResponseModel>> CreateTransactionAsync([Body] CreateTransactionModel payload);

	[Get("/{txId}")]
	Task<ApiResponse<TransactionDetailsModel>> GetTransactionByIdAsync(string txId);

	[Get("/external_tx_id/{externalTxId}")]
	Task<ApiResponse<TransactionDetailsModel>> GetTransactionByExternalTxIdAsync(string externalTxId);

	[Post("/{txId}/cancel")]
	Task<ApiResponse<string>> CancelTransactionByIdAsync(string txId);

	[Post("/{txId}/drop")]
	Task<ApiResponse<string>> DropTransactionAsync(string txId, [Body] DropTransactionModel payload);

	[Post("/{txId}/freeze")]
	Task<ApiResponse<string>> FreezeTransactionAsync(string txId);

	[Post("/{txId}/unfreeze")]
	Task<ApiResponse<string>> UnfreezeTransactionAsync(string txId);

	[Get("/validate_address/{assetId}/{address}")]
	Task<ApiResponse<AddressStatusModel>> ValidateDestinationAddressAsync(string assetId, string address);

	[Post("/estimate_fee")]
	Task<ApiResponse<EstimatedTransactionFeeResponseModel>> EstimateFeeForTransactionAsync(
		[Body] EstimateTransactionFeeModel payload);

	[Post("/{txId}/set_confirmation_threshold")]
	Task<ApiResponse<SetConfirmationsThresholdResponseModel>> SetConfirmationThresholdForTxIdAsync(
		string txId,
		[Body] SetConfirmationThresholdByTxHashModel payload);

	[Post("/{txHash}/set_confirmation_threshold")]
	Task<ApiResponse<SetConfirmationsThresholdResponseModel>> SetConfirmationThresholdForTxHashAsync(
		string txHash,
		[Body] SetConfirmationThresholdByTxHashModel payload);
}
