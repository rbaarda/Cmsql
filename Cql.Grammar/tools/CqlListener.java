// Generated from ..\Cql.g4 by ANTLR 4.7
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link CqlParser}.
 */
public interface CqlListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link CqlParser#queries}.
	 * @param ctx the parse tree
	 */
	void enterQueries(CqlParser.QueriesContext ctx);
	/**
	 * Exit a parse tree produced by {@link CqlParser#queries}.
	 * @param ctx the parse tree
	 */
	void exitQueries(CqlParser.QueriesContext ctx);
	/**
	 * Enter a parse tree produced by {@link CqlParser#query}.
	 * @param ctx the parse tree
	 */
	void enterQuery(CqlParser.QueryContext ctx);
	/**
	 * Exit a parse tree produced by {@link CqlParser#query}.
	 * @param ctx the parse tree
	 */
	void exitQuery(CqlParser.QueryContext ctx);
	/**
	 * Enter a parse tree produced by {@link CqlParser#selectClause}.
	 * @param ctx the parse tree
	 */
	void enterSelectClause(CqlParser.SelectClauseContext ctx);
	/**
	 * Exit a parse tree produced by {@link CqlParser#selectClause}.
	 * @param ctx the parse tree
	 */
	void exitSelectClause(CqlParser.SelectClauseContext ctx);
	/**
	 * Enter a parse tree produced by {@link CqlParser#fromClause}.
	 * @param ctx the parse tree
	 */
	void enterFromClause(CqlParser.FromClauseContext ctx);
	/**
	 * Exit a parse tree produced by {@link CqlParser#fromClause}.
	 * @param ctx the parse tree
	 */
	void exitFromClause(CqlParser.FromClauseContext ctx);
	/**
	 * Enter a parse tree produced by {@link CqlParser#whereClause}.
	 * @param ctx the parse tree
	 */
	void enterWhereClause(CqlParser.WhereClauseContext ctx);
	/**
	 * Exit a parse tree produced by {@link CqlParser#whereClause}.
	 * @param ctx the parse tree
	 */
	void exitWhereClause(CqlParser.WhereClauseContext ctx);
	/**
	 * Enter a parse tree produced by the {@code binaryExpression}
	 * labeled alternative in {@link CqlParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterBinaryExpression(CqlParser.BinaryExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code binaryExpression}
	 * labeled alternative in {@link CqlParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitBinaryExpression(CqlParser.BinaryExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code conditionExpression}
	 * labeled alternative in {@link CqlParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterConditionExpression(CqlParser.ConditionExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code conditionExpression}
	 * labeled alternative in {@link CqlParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitConditionExpression(CqlParser.ConditionExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code parenthesizedExpression}
	 * labeled alternative in {@link CqlParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterParenthesizedExpression(CqlParser.ParenthesizedExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code parenthesizedExpression}
	 * labeled alternative in {@link CqlParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitParenthesizedExpression(CqlParser.ParenthesizedExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link CqlParser#condition}.
	 * @param ctx the parse tree
	 */
	void enterCondition(CqlParser.ConditionContext ctx);
	/**
	 * Exit a parse tree produced by {@link CqlParser#condition}.
	 * @param ctx the parse tree
	 */
	void exitCondition(CqlParser.ConditionContext ctx);
}